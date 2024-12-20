import { Component, Input } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AsyncSelectComponent } from '../../../../infrastructure/components/async-select/async-select.component';
import { TerrainDto } from '../../../nomenclature/dtos/terrain.dto';
import { CollapsableLabelComponent } from '../../../../infrastructure/components/collapsable-label/collapsable-label.component';
import { SvgIconComponent } from '../../../../infrastructure/components/svg-icon/svg-icon.component';
import { TerrainResource } from '../../resources/terrain.resource';
import { ToastrService } from 'ngx-toastr';
import { catchError, finalize, throwError } from 'rxjs';
import { LoadingIndicatorService } from '../../../../infrastructure/components/loading-indicator/services/loading-indicator.service';
import { OperatorResource } from '../../resources/operator.resource';
import { SettlementChangeService } from '../../../../infrastructure/services/settlement-change.service';

@Component({
  selector: 'operator-terrains',
  standalone: true,
  imports: [
    TranslateModule,
    CommonModule,
    FormsModule,
    AsyncSelectComponent,
    CollapsableLabelComponent,
    SvgIconComponent
  ],
  providers: [
    TerrainResource,
    OperatorResource,
    SettlementChangeService
  ],
  templateUrl: './operator-terrains.component.html',
  styleUrl: './operator-terrains.component.css'
})
export class OperatorTerrainsComponent {
  @Input() terrains: TerrainDto[];
  @Input() operatorId: number;

  isTerrainFormDisplayed: boolean = false;

  newTerrain: TerrainDto;
  terrainCopies: TerrainDto[];

  constructor(
    private resource: TerrainResource,
    private operatorResource: OperatorResource,
    private toastrService: ToastrService,
    private loadingIndicator: LoadingIndicatorService,
    public settlementChangeService: SettlementChangeService
  ) { }

  displayTerrainForm(): void {
    this.isTerrainFormDisplayed = true;
    this.newTerrain = new TerrainDto();
    this.newTerrain.operatorId = this.operatorId;
  }

  addTerrain(): void {
    this.loadingIndicator.show();

    this.resource.add(this.newTerrain)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe(() => {
        this.toastrService.success('Успешно добавяне на площадка!');
        this.cancelAddingTerrain();

        this.operatorResource.getById(this.operatorId)
          .subscribe((result) => {
            this.terrains = result.terrains;
            this.copyTerrains();
          });
      });
  }

  cancelAddingTerrain(): void {
    this.isTerrainFormDisplayed = false;
    this.newTerrain = null;
  }

  enableTerrainModification(terrain: TerrainDto): void {
    if (this.terrainCopies === undefined ||
      this.terrainCopies === null ||
      this.terrainCopies?.length === 0) {
      this.copyTerrains();
    }

    terrain.isInEditMode = true;
  }

  cancelTerrainModification(terrain: TerrainDto): void {
    terrain.isInEditMode = false;
    let oldTerrain = this.terrainCopies.find(t => t.id === terrain.id);
    let modifiedTerrainIndex = this.terrains.findIndex(t => t.id === terrain.id);

    this.terrains[modifiedTerrainIndex] = JSON.parse(JSON.stringify(oldTerrain));
  }

  modifyTerrain(terrain: TerrainDto): void {
    this.loadingIndicator.show();

    this.resource.update(terrain)
      .pipe(
        finalize(() => this.loadingIndicator.hide()),
        catchError((error: any) => {
          this.cancelTerrainModification(terrain);
          return throwError(error);
        })
      )
      .subscribe(() => {
        this.toastrService.success('Успешно редактиране на площадка!');

        let modifiedTerrainIndex = this.terrains.findIndex(t => t.id === terrain.id);
        this.terrainCopies[modifiedTerrainIndex] = JSON.parse(JSON.stringify(terrain));
        this.terrains[modifiedTerrainIndex].isInEditMode = false;
      });
  }

  private copyTerrains(): void {
    if (this.terrains?.length > 0) {
      this.terrainCopies = JSON.parse(JSON.stringify(this.terrains));
    }
  }
}
