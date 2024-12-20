import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { BaseNomenclatureComponent } from '../common/components/base-nomenclature.component';
import { FormsModule } from '@angular/forms';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { SvgIconComponent } from '../../../infrastructure/components/svg-icon/svg-icon.component';
import { CommonModule } from '@angular/common';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Sector } from '../models/sector.model';
import { BaseNomenclatureFilterDto } from '../common/models/base-nomenclature-filter.dto';
import { NomenclatureResource } from '../common/resources/nomenclature.resource';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-sector',
  standalone: true,
  imports: [TranslateModule, SvgIconComponent, FormsModule, CommonModule, NgbModule],
  templateUrl: './../common/components/base-nomenclature.component.html',
})

export class SectorComponent extends BaseNomenclatureComponent<Sector, BaseNomenclatureFilterDto> implements OnInit {

  ngOnInit(): void {
    this.init(Sector, BaseNomenclatureFilterDto, 'Sector');
  }
}