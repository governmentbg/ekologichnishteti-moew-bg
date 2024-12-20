import { HttpClient } from '@angular/common/http';
import { Component, ContentChild, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { NgbDropdown, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { merge, of, Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged, finalize, switchMap } from 'rxjs/operators';
import { Nomenclature } from '../../../modules/nomenclature/common/models/nomenclature.model';
import { Configuration } from '../../configuration/configuration';
import { CommonModule } from '@angular/common';
import { SvgIconComponent } from '../svg-icon/svg-icon.component';
import { BaseNomenclatureFilterDto } from '../../../modules/nomenclature/common/models/base-nomenclature-filter.dto';

@Component({
  selector: 'app-async-select',
  standalone: true,
  imports: [CommonModule, NgbModule, SvgIconComponent, FormsModule],
  templateUrl: './async-select.component.html',
  styleUrls: ['./async-select.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: AsyncSelectComponent,
      multi: true
    }
  ]
})
export class AsyncSelectComponent<T extends Nomenclature> implements OnInit, OnDestroy {
  @Input() restUrl: string;
  @Input() placeholder: string;

  private filter = new BaseNomenclatureFilterDto();
  @Input('filter') set filterSetter(filter: any) {
    if (filter) {
      this.filter = filter;
      this.collection = [];
    }
  }

  inputTextFilter: string = '';

  @Input() limit = 10;

  @Input() disabled: boolean;

  @Input() required: boolean;

  @Input() checkKeys = ['id'];

  @Input() enableBorder: boolean = false;

  @Input() allowClear = false;

  @Input() includeInactive = false;

  @Input() excludeAlreadyAdded: boolean = false;

  @Input() params: any[];

  @Input() collection: T[] = [];

  @Output() modelChange: EventEmitter<T> = new EventEmitter<T>();

  @ContentChild('dropdownItemTemplate', { static: true }) dropdownItemTemplate: TemplateRef<ElementRef>;
  @ContentChild('selectedItemTemplate', { static: true }) selectedItemTemplate: TemplateRef<ElementRef>;

  @ViewChild('searchField', { static: false }) searchField: ElementRef;
  @ViewChild('dropdown', { static: true }) dropdown: NgbDropdown;
  @ViewChild('dropdownMenu', { static: true }) dropdownMenu: ElementRef;

  constructor(
    private http: HttpClient,
    private config: Configuration
  ) { }

  selectedItem: T | null;
  displayValue: string = null;
  searchControl = new FormControl('');
  isOpen = false;
  isLoading = false;
  formControlChangeHandler: any;

  hasResult: boolean = true;

  private searchSubscription: Subscription;
  private loadMore$ = new EventEmitter<boolean>();
  private hasMore = false;

  search$ = merge(
    this.searchControl.valueChanges.pipe(
      distinctUntilChanged()
    ),
    this.loadMore$,
  ).pipe(
    debounceTime(300),
    switchMap(value => {
      const isValueBoolean = typeof value === 'boolean';
      this.filter.textFilter = isValueBoolean ? this.filter.textFilter : value;
      this.filter.offset = isValueBoolean ? this.filter.offset : 0;
      this.filter.limit = this.limit;
      return of(this.filter);
    })
  );

  inputHandle() {
    this.filter.textFilter = this.inputTextFilter;
    this.filter.limit = this.limit;
    this.filter.offset = 0;

    this.getFiltered(this.filter);
  }

  ngOnInit(): void {
    this.dropdown.openChange
      .subscribe((isOpen: boolean) => {
        if (this.disabled) {
          return;
        }

        this.isOpen = isOpen;

        if (this.isOpen) {
          this.searchControl.setValue('', { emitEvent: false });
          setTimeout(() => this.searchField?.nativeElement.focus(), 1);

          if (this.restUrl === undefined) {
            return;
          }

          this.filter.textFilter = '';
          this.filter.limit = this.limit;
          this.filter.offset = 0;

          this.getFiltered(this.filter);
        }
        else {
          this.inputTextFilter = '';
        }
      });

    this.searchSubscription = this.search$.subscribe(filter => this.getFiltered(filter));
  }

  ngOnDestroy(): void {
    if (!this.searchSubscription.closed) {
      this.searchSubscription.unsubscribe();
    }
  }

  registerOnChange(fn: any): void {
    this.formControlChangeHandler = fn;
  }

  registerOnTouched(fn: any): void { }

  writeValue(item: T): void {
    this.selectedItem = item;
    this.displayValue = this.getDisplayValue(item);
  }

  handleScroll(event: WheelEvent): void {
    const isAtBottom = this.isAtBottom(event, this.dropdownMenu.nativeElement);

    if (isAtBottom && this.hasMore && !this.isLoading) {
      event.preventDefault();

      this.filter.offset = this.collection.length;
      this.loadMore$.emit(true);
    }
  }

  clear(event: Event): void {
    this.selectItem(null);
    event.preventDefault();
    event.stopPropagation();
  }

  selectItem(item: T): void {
    this.selectedItem = item;
    this.displayValue = this.getDisplayValue(item);
    this.modelChange.emit(item);

    if (this.formControlChangeHandler) {
      this.formControlChangeHandler(item);
    }
  }

  isSelectedItem(item: T): boolean {
    if (!this.selectedItem || !item) {
      return false;
    }

    let result = true;
    this.checkKeys.forEach((key: string) => {
      const selectedItemNestedValue = this.getNestedValue(key, this.selectedItem);
      const itemNestedValue = this.getNestedValue(key, item);
      result = result && (selectedItemNestedValue === itemNestedValue);
    });

    return result;
  }

  private getNestedValue(property: string, item: T): any {
    let result = item;
    const nestedProperties = property.split('.');
    if (nestedProperties?.length > 0) {
      for (let i = 0; i <= nestedProperties.length - 1; i++) {
        result = result[nestedProperties[i]];
        if (!result) {
          return null;
        }
      }
    } else {
      result = result[property];
    }

    return result;
  }

  private getFiltered(filter?: BaseNomenclatureFilterDto): void {
    if (!filter) {
      filter = new BaseNomenclatureFilterDto();
      filter.textFilter = '';
      filter.limit = this.limit;
      filter.offset = 0;
    }

    this.filter.isActive = this.includeInactive ? null : true;
    this.filter.excludeAlreadyAdded = this.excludeAlreadyAdded;

    this.isLoading = true;
    this.http
      .get<T[]>(`${this.config.restUrl}/${this.restUrl}?${this.getQueryString(filter)}`, this.params ? { params: { ids: this.params } } : {})
      .pipe(
        finalize(() => this.isLoading = false)
      )
      .subscribe(items => {
        if (filter.offset === 0) {
          this.collection = items;
          if (this.collection.length < 1) {
            this.hasResult = false;
          } else {
            this.hasResult = true;
          }
        }
        else {
          this.collection.push(...items);
        }

        this.hasMore = items.length > 0;
      });
  }

  private getQueryString(filter?: BaseNomenclatureFilterDto): string {
    let queryString = '';
    if (filter) {
      Object.keys(filter)
        .filter((key: string) => filter[key] !== null && filter[key] !== undefined)
        .forEach((key: string, index: number) => {
          queryString += index === 0 ? '' : '&';
          queryString += `${key}=${filter[key]}`;
        });
    }

    return queryString;
  }

  private getDisplayValue(item: T | null): string | null {
    if (!item) {
      return null;
    }

    return item.name;
  }

  private isAtBottom(event: WheelEvent, target: HTMLElement): boolean {
    const atBottom = (target.scrollHeight - Math.ceil(target.scrollTop)) === target.clientHeight || (target.scrollHeight - Math.ceil(target.scrollTop)) - 1 === target.clientHeight;

    return event.deltaY > 0 && atBottom;
  }
}