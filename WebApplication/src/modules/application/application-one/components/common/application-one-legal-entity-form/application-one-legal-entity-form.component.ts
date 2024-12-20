import { Component, Input, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonFormComponent } from '../../../../../../infrastructure/components/common-form/common-form.component';
import { SharedService } from '../../../../../../infrastructure/services/shared-service';
import { ApplicationOneDto } from '../../../models/application-one.dto';
import { ApplicantType } from '../../../enums/applicant-type.enum';

@Component({
  selector: 'app-application-one-legal-entity-form',
  standalone: true,
  imports: [
    FormsModule,
    TranslateModule
  ],
  templateUrl: './application-one-legal-entity-form.component.html',
  styleUrl: './application-one-legal-entity-form.component.css'
})
export class ApplicationOneLegalEntityFormComponent extends CommonFormComponent<ApplicationOneDto> implements OnInit {
  @Input() isViewMode: boolean;

  applicantTypes = ApplicantType;

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
