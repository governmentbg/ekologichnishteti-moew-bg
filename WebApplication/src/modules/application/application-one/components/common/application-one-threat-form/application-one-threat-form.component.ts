import { Component, Input, OnInit } from '@angular/core';
import { CommonFormComponent } from '../../../../../../infrastructure/components/common-form/common-form.component';
import { SharedService } from '../../../../../../infrastructure/services/shared-service';
import { ApplicationOneDto } from '../../../models/application-one.dto';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-application-one-threat-form',
  standalone: true,
  imports: [FormsModule, TranslateModule],
  templateUrl: './application-one-threat-form.component.html',
  styleUrl: './application-one-threat-form.component.css'
})
export class ApplicationOneThreatFormComponent extends CommonFormComponent<ApplicationOneDto> implements OnInit {
  @Input() isViewMode: boolean;

  maxTextLength1024 = 1024;

  constructor(public sharedService: SharedService) {
    super();
  }

  ngOnInit(): void {
    if (!this.isViewMode) {
      this.model.clearOptionals();
    }
  }
}

