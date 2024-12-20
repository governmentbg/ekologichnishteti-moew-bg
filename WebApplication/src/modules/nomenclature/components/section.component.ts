import { Component, OnInit } from '@angular/core';
import { Section } from '../models/section.model';
import { BaseNomenclatureComponent } from '../common/components/base-nomenclature.component';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { SvgIconComponent } from '../../../infrastructure/components/svg-icon/svg-icon.component';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BaseNomenclatureFilterDto } from '../common/models/base-nomenclature-filter.dto';

@Component({
  selector: 'app-section',
  standalone: true,
  imports: [TranslateModule, SvgIconComponent, FormsModule, CommonModule, NgbModule],
  templateUrl: './../common/components/base-nomenclature.component.html',
})

export class SectionComponent extends BaseNomenclatureComponent<Section, BaseNomenclatureFilterDto> implements OnInit {
  ngOnInit(): void {
    this.init(Section, BaseNomenclatureFilterDto, 'Section');
  }
}