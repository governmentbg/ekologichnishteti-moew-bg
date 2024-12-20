import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { TranslateModule } from "@ngx-translate/core";
import { SvgIconComponent } from "../../../infrastructure/components/svg-icon/svg-icon.component";
import { BaseNomenclatureComponent } from "../common/components/base-nomenclature.component";
import { BaseNomenclatureFilterDto } from "../common/models/base-nomenclature-filter.dto";
import { Country } from "../models/country.model";

@Component({
  selector: 'app-country',
  standalone: true,
  imports: [TranslateModule, SvgIconComponent, FormsModule, CommonModule, NgbModule],
  templateUrl: './../common/components/base-nomenclature.component.html',
})

export class CountryComponent extends BaseNomenclatureComponent<Country, BaseNomenclatureFilterDto> implements OnInit {
  ngOnInit(): void {
    this.init(Country, BaseNomenclatureFilterDto, 'Country');
  }
}