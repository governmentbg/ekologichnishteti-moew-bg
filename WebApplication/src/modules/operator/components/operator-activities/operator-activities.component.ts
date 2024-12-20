import { Component, Input } from '@angular/core';
import { CollapsableLabelComponent } from '../../../../infrastructure/components/collapsable-label/collapsable-label.component';
import { AsyncSelectComponent } from '../../../../infrastructure/components/async-select/async-select.component';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule } from '@angular/forms';
import { ActivityOfferingDto } from '../../../nomenclature/dtos/activity-offering.dto';
import { OperatorResource } from '../../resources/operator.resource';
import { ToastrService } from 'ngx-toastr';
import { LoadingIndicatorService } from '../../../../infrastructure/components/loading-indicator/services/loading-indicator.service';
import { ActivityOfferingResource } from '../../resources/activity-offering.resource';
import { catchError, finalize, throwError } from 'rxjs';
import { SvgIconComponent } from '../../../../infrastructure/components/svg-icon/svg-icon.component';
import { TerrainDto } from '../../../nomenclature/dtos/terrain.dto';
import { AuthorityType } from '../../../application/application-one/enums/authority-type.enum';

@Component({
  selector: 'operator-activities',
  standalone: true,
  imports: [
    CollapsableLabelComponent,
    AsyncSelectComponent,
    TranslateModule,
    CommonModule,
    FormsModule,
    SvgIconComponent
  ],
  providers: [
    ActivityOfferingResource
  ],
  templateUrl: './operator-activities.component.html',
  styleUrl: './operator-activities.component.css'
})
export class OperatorActivitiesComponent {
  @Input() activityOfferings: ActivityOfferingDto[];
  @Input() operatorId: number;
  @Input() terrains: TerrainDto[];

  authorityType = AuthorityType;

  isActivityOfferingFormDisplayed: boolean = false;
  canSubmit: boolean = false;

  newActivityOffering: ActivityOfferingDto;
  activityOfferingCopies: ActivityOfferingDto[];

  constructor(
    private resource: ActivityOfferingResource,
    private operatorResource: OperatorResource,
    private toastrService: ToastrService,
    private loadingIndicator: LoadingIndicatorService
  ) { }

  ngDoCheck(): void {
    this.canSubmit = this.newActivityOffering?.activityId !== undefined &&
      this.newActivityOffering?.subActivityId !== undefined;
  }

  displayActivityOfferingForm(): void {
    this.isActivityOfferingFormDisplayed = true;
    this.newActivityOffering = new ActivityOfferingDto();
    this.newActivityOffering.operatorId = this.operatorId;
  }

  addActivityOffering(): void {
    this.loadingIndicator.show();

    this.resource.add(this.newActivityOffering)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe(() => {
        this.toastrService.success('Успешно добавяне на дейност!');
        this.cancelAddingActivityOffering();

        this.operatorResource.getById(this.operatorId)
          .subscribe((result) => {
            this.activityOfferings = result.activityOfferings;
            this.copyActivityOfferings();
          });
      });
  }

  cancelAddingActivityOffering(): void {
    this.isActivityOfferingFormDisplayed = false;
    this.newActivityOffering = null;
  }

  onActivityChange(activity: any): void {
    this.newActivityOffering.activityId = activity?.id;
    this.newActivityOffering.subActivityId = undefined;
    this.newActivityOffering.subActivity = null;
  }

  onAddActivityOfferingTerrainChange(terrain: any): void {
    this.newActivityOffering.terrainId = terrain.id;
  }

  onModifyActivityOfferingTerrainChange(activityOffering: ActivityOfferingDto, terrain: any): void {
    let activityOfferingIndex = this.activityOfferings.find(a => a.activityId === activityOffering.id).id;
    this.activityOfferings[activityOfferingIndex].terrainId = terrain.id;
  }


  enableActivityOfferingModification(activityOffering: ActivityOfferingDto): void {
    if (this.activityOfferingCopies === undefined ||
      this.activityOfferingCopies === null ||
      this.activityOfferingCopies?.length === 0) {
      this.copyActivityOfferings();
    }

    activityOffering.isInEditMode = true;
  }

  cancelActivityOfferingModification(activityOffering: ActivityOfferingDto): void {
    activityOffering.isInEditMode = false;
    let oldActivityOffering = this.activityOfferingCopies.find(a => a.id === activityOffering.id);
    let modifiedActivityOfferingIndex = this.activityOfferingCopies.findIndex(a => a.id === activityOffering.id);

    this.activityOfferings[modifiedActivityOfferingIndex] = JSON.parse(JSON.stringify(oldActivityOffering));
  }

  modifyActivityOffering(activityOffering: ActivityOfferingDto): void {
    this.loadingIndicator.show();

    this.resource.update(activityOffering)
      .pipe(
        finalize(() => this.loadingIndicator.hide()),
        catchError((error: any) => {
          this.cancelActivityOfferingModification(activityOffering);
          return throwError(error);
        })
      )
      .subscribe(() => {
        this.toastrService.success('Успешно редактиране на дейност!');

        let modifiedActivityOfferingIndex = this.activityOfferings.findIndex(a => a.id === activityOffering.id);
        this.activityOfferingCopies[modifiedActivityOfferingIndex] = JSON.parse(JSON.stringify(activityOffering));
        this.activityOfferings[modifiedActivityOfferingIndex].isInEditMode = false;
      });
  }

  private copyActivityOfferings(): void {
    if (this.activityOfferings?.length > 0) {
      this.activityOfferingCopies = JSON.parse(JSON.stringify(this.activityOfferings));
    }
  }
}
