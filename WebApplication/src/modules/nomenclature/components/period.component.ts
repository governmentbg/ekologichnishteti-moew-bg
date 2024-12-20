import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { TranslateModule } from "@ngx-translate/core";
import { SvgIconComponent } from "../../../infrastructure/components/svg-icon/svg-icon.component";
import { BaseNomenclatureComponent } from "../common/components/base-nomenclature.component";
import { BaseNomenclatureFilterDto } from "../common/models/base-nomenclature-filter.dto";
import { Period } from "../models/period.model";

@Component({
  selector: 'app-period',
  standalone: true,
  imports: [TranslateModule, SvgIconComponent, FormsModule, CommonModule, NgbModule],
  templateUrl: './../common/components/base-nomenclature.component.html',
})

export class PeriodComponent extends BaseNomenclatureComponent<Period, BaseNomenclatureFilterDto> implements OnInit {
  ngOnInit(): void {
    this.init(Period, BaseNomenclatureFilterDto, 'Period');
  }
}