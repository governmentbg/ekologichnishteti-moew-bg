import { Component, OnInit } from '@angular/core';
import { BaseNomenclatureComponent } from '../../common/components/base-nomenclature.component';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { SvgIconComponent } from '../../../../infrastructure/components/svg-icon/svg-icon.component';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Code } from '../../models/code.model';
import { CodeSearchFilter } from '../../services/code-search.filter';

@Component({
  selector: 'app-code',
  standalone: true,
  imports: [TranslateModule, SvgIconComponent, FormsModule, CommonModule, NgbModule],
  templateUrl: './code.component.html',
})

export class CodeComponent extends BaseNomenclatureComponent<Code, CodeSearchFilter> implements OnInit {
  ngOnInit(): void {
    this.search();
  }

  loadAll() {
    this.filter.limit = this.totalCounts;
    this.loadMore();
  }

  clearFilter(): void {
    this.filter.name = null;

    this.search();
  }

  search(): void {
    this.resetFilterLimitAndOffset();
    this.init(Code, CodeSearchFilter, 'Code');
  }

  private resetFilterLimitAndOffset(): void {
    if (this.filter) {
      this.filter.limit = 10;
      this.filter.offset = 0;
    }
  }
}