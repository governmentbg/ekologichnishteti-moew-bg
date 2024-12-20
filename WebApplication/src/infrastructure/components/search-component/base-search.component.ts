import { Directive, EventEmitter, HostListener, OnInit, Output } from '@angular/core';
import { Subscription, finalize } from 'rxjs';
import { LoadingIndicatorService } from '../loading-indicator/services/loading-indicator.service';
import { IFilterable } from '../../interfaces/filterable.interface';
import { BaseSearchFilter } from '../../models/base-search.filter';
import { SearchResultItemDto } from '../../models/search-result-item.dto';
import { ContentTypes } from '../../constants/constants';

@Directive({
  standalone: true
})
export abstract class BaseSearchComponent<TDto> implements OnInit {

  @HostListener('document:keypress', ['$event'])
  handleKeyboardEvent(event: KeyboardEvent) {
    if (event.key === 'Enter') {
      this.search();
    }
  }

  @Output() searchEvent = new EventEmitter<TDto[]>();

  model: TDto[] = [];
  canLoadMore: boolean;
  contentTypes = ContentTypes;
  modelCounts = 0;
  totalCounts = 0;

  constructor(
    public filter: BaseSearchFilter,
    protected resource: IFilterable<BaseSearchFilter, SearchResultItemDto<TDto>>,
    protected loadingIndicator: LoadingIndicatorService
  ) { }

  ngOnInit(): void {
    this.search();
  }

  search(loadMore?: boolean): Subscription {
    this.loadingIndicator.show();

    return this.resource.getFiltered(this.filter)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((result: SearchResultItemDto<TDto>) => {
        this.totalCounts = result?.totalCount;

        if (!this.filter.offset) {
          this.modelCounts = result?.items?.length;
          this.model = result?.items;
        } else {
          this.modelCounts = this.modelCounts + result?.items?.length;
          this.model = [...this.model, ...result?.items];
        }

        this.canLoadMore = this.modelCounts > 0 && this.modelCounts < this.totalCounts;

        if (!this.canLoadMore) {
          this.filter.offset = 0;
        }

        this.searchEvent.emit(this.model);
      });
  }

  loadMore(): void {
    this.filter.offset = this.model.length;
    this.search(true);
  }

  clearFilter(): void {
    this.filter.clear();
    this.modelCounts = 0;
    this.search();
  }
}
