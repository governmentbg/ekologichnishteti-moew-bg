import { ChangeDetectorRef, Component } from '@angular/core';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { BaseNomenclatureFilterDto } from '../models/base-nomenclature-filter.dto';
import { Nomenclature } from '../models/nomenclature.model';
import { NomenclatureResource } from '../resources/nomenclature.resource';
import { SvgIconComponent } from '../../../../infrastructure/components/svg-icon/svg-icon.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SearchResultItemDto } from '../../../../infrastructure/models/search-result-item.dto';
import { RoleService } from '../../../../infrastructure/services/role.service';
import { UserRoleAliases } from '../../../../infrastructure/constants/constants';

@Component({
  selector: 'base-nomenclature-component',
  standalone: true,
  templateUrl: './base-nomenclature.component.html',
  imports: [TranslateModule, SvgIconComponent, FormsModule, CommonModule, NgbModule]
})
export abstract class BaseNomenclatureComponent<T extends Nomenclature, TFilter extends BaseNomenclatureFilterDto> {
  isAdmin: boolean = false;

  model: T[] = [];
  modelCounts = 0;
  totalCounts = 0;

  canLoadMore: boolean;
  currentPrefix: string;
  private structuredFilterType: (new () => TFilter);

  filter: TFilter;

  private type: (new () => T);

  constructor(
    protected roleService: RoleService,
    protected resource: NomenclatureResource<T, TFilter>,
    protected cd: ChangeDetectorRef,
    protected modal: NgbModal,
    protected translateService: TranslateService,
    protected toastrService: ToastrService
  ) {
  }

  protected init(type: (new () => T), structuredFilterType: (new () => TFilter), prefix: string): void {
    this.isAdmin = this.roleService.hasRole(UserRoleAliases.ADMINISTRATOR);

    this.structuredFilterType = structuredFilterType;

    if (!this.filter) {
      this.filter = new this.structuredFilterType();
    }

    if (!this.canLoadMore) {
      this.filter.offset = 0;
    }

    this.type = type;
    this.currentPrefix = prefix;
    this.resource.setSuffix(prefix);

    this.filter.includeInactive = true;
    this.resource.getFiltered(this.filter)
      .subscribe((result: SearchResultItemDto<T>) => {
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

        this.cd.markForCheck();
      });
  }

  loadMore(): void {
    this.filter.offset = this.model.length;
    this.init(this.type, this.structuredFilterType, this.currentPrefix);
  }

  add(): void {
    if (this.model.filter(e => e.isEditMode).length) {
      return;
    }

    const newEntity = new this.type();
    newEntity.isActive = true;
    newEntity.isEditMode = true;

    this.model.unshift(newEntity);
  }

  edit(item: T): void {
    item.originalObject = Object.assign({}, item);
    item.isEditMode = true;
  }

  cancel(item: T, index: number): void {
    if (!item.id) {
      this.model.splice(index, 1);
    } else {
      Object.keys(item).forEach(key => {
        if (key !== 'originalObject') {
          item[key] = item.originalObject[key];
        }
      });

      item.isEditMode = false;
      item.originalObject = null;
    }
  }

  save(item: T, index: number): void {
    if (!item.id) {
      this.resource.add(item)
        .subscribe((result: T) => {
          item.originalObject = null;

          this.model[index] = result;
          this.cd.markForCheck();
        });
    } else {
      this.resource.update(item.id, item)
        .subscribe((result: T) => {
          item.originalObject = null;

          this.model[index] = result;
          this.cd.markForCheck();
        });
    }
  }

  // delete(id: number, index: number): void {
  //   const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
  //   const confirmationMessage = "Сигурни ли сте, че искате да изтриете стойността?";
  //   confirmationModal.componentInstance.confirmationMessage = confirmationMessage;
  //   confirmationModal.result
  //     .then((result: boolean) => {
  //       if (result) {
  //         this.resource.delete(id)
  //           .subscribe(() => {
  //             this.model.splice(index, 1);
  //             this.cd.markForCheck();
  //           },
  //             (err) => handleDomainError(
  //               err,
  //               [{ code: 'Nomenclature_CannotDelete', text: this.translateService.instant('nomenclature.cannotDelete') }],
  //               this.toastrService
  //             )
  //           );
  //       }
  //     });
  // }
}
