import { Component, OnInit } from '@angular/core';
import { BaseNomenclatureComponent } from '../common/components/base-nomenclature.component';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { SvgIconComponent } from '../../../infrastructure/components/svg-icon/svg-icon.component';
import { CommonModule } from '@angular/common';
import { Group } from '../models/group.model';
import { BaseNomenclatureFilterDto } from '../common/models/base-nomenclature-filter.dto';

@Component({
  selector: 'app-group',
  standalone: true,
  imports: [TranslateModule, SvgIconComponent, FormsModule, CommonModule],
  templateUrl: './../common/components/base-nomenclature.component.html',
})

export class GroupComponent extends BaseNomenclatureComponent<Group, BaseNomenclatureFilterDto> implements OnInit {
  ngOnInit(): void {
    this.init(Group, BaseNomenclatureFilterDto, 'Group');
  }
}