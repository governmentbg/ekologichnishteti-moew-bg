import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { TranslateModule } from "@ngx-translate/core";
import { SvgIconComponent } from "../../../infrastructure/components/svg-icon/svg-icon.component";
import { BaseNomenclatureComponent } from "../common/components/base-nomenclature.component";
import { BaseNomenclatureFilterDto } from "../common/models/base-nomenclature-filter.dto";
import { Year } from "../models/year.model";

@Component({
  selector: 'app-year',
  standalone: true,
  imports: [TranslateModule, SvgIconComponent, FormsModule, CommonModule, NgbModule],
  templateUrl: './../common/components/base-nomenclature.component.html',
})

export class YearComponent extends BaseNomenclatureComponent<Year, BaseNomenclatureFilterDto> implements OnInit {
  ngOnInit(): void {
    this.init(Year, BaseNomenclatureFilterDto, 'Year');
  }
}