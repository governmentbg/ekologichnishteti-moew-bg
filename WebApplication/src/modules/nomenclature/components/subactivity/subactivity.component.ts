import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { SvgIconComponent } from '../../../../infrastructure/components/svg-icon/svg-icon.component';
import { SubActivity } from '../../models/sub-activity.model';
import { BaseNomenclatureComponent } from '../../common/components/base-nomenclature.component';
import { RoleService } from '../../../../infrastructure/services/role.service';
import { NomenclatureResource } from '../../common/resources/nomenclature.resource';
import { ToastrService } from 'ngx-toastr';
import { Activity } from '../../models/activity.model';
import { BaseNomenclatureFilterDto } from '../../common/models/base-nomenclature-filter.dto';
import { SubActivitySearchFilter } from '../../services/sub-activity-search.filter';

@Component({
  selector: 'app-subactivity',
  standalone: true,
  imports: [
    TranslateModule,
    SvgIconComponent,
    FormsModule,
    CommonModule,
    NgbModule
  ],
  templateUrl: './subactivity.component.html'
})

export class SubActivityComponent extends BaseNomenclatureComponent<SubActivity, SubActivitySearchFilter> implements OnInit {
  activityIds: number[];

  constructor(
    protected roleService: RoleService,
    protected resource: NomenclatureResource<SubActivity, SubActivitySearchFilter>,
    protected cd: ChangeDetectorRef,
    protected modal: NgbModal,
    protected translateService: TranslateService,
    protected toastrService: ToastrService,
    protected activityResource: NomenclatureResource<Activity, BaseNomenclatureFilterDto>
  ) {
    super(roleService, resource, cd, modal, translateService, toastrService);
  }

  ngOnInit(): void {
    this.search();
  }

  loadAll() {
    this.filter.limit = this.totalCounts;
    this.loadMore();
  }

  clearFilter(): void {
    this.filter.code = null;
    this.filter.name = null;
    this.filter.activityId = null;

    this.search();
  }

  search(): void {
    this.resetFilterLimitAndOffset();
    this.init(SubActivity, SubActivitySearchFilter, 'SubActivity');
    this.getActivityIds();
  }

  getActivityIds(): void {
    let filter = new BaseNomenclatureFilterDto();
    filter.limit = null;
    filter.offset = null;

    this.activityResource.setSuffix('Activity');
    this.activityResource.getFiltered(filter).subscribe((result) => {
      this.activityIds = result.items.map(i => i.id);
    });
  }

  private resetFilterLimitAndOffset(): void {
    if (this.filter) {
      this.filter.limit = 10;
      this.filter.offset = 0;
    }
  }
}