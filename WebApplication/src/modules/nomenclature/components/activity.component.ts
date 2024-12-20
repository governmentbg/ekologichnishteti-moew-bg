import { Component, OnInit } from '@angular/core';
import { BaseNomenclatureComponent } from '../common/components/base-nomenclature.component';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { SvgIconComponent } from '../../../infrastructure/components/svg-icon/svg-icon.component';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Activity } from '../models/activity.model';
import { BaseNomenclatureFilterDto } from '../common/models/base-nomenclature-filter.dto';

@Component({
  selector: 'app-danger-cause',
  standalone: true,
  imports: [TranslateModule, SvgIconComponent, FormsModule, CommonModule, NgbModule],
  templateUrl: './../common/components/base-nomenclature.component.html',
})

export class ActivityComponent extends BaseNomenclatureComponent<Activity, BaseNomenclatureFilterDto> implements OnInit {
  ngOnInit(): void {
    this.init(Activity, BaseNomenclatureFilterDto, 'Activity');
  }
}