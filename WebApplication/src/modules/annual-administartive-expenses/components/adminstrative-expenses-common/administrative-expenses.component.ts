import { Component } from "@angular/core";
import { TranslateModule } from "@ngx-translate/core";
import { CollapsableLabelComponent } from "../../../../infrastructure/components/collapsable-label/collapsable-label.component";
import { PeriodsComponent } from "../periods/periods.component";

@Component({
  selector: 'administrative-expenses-component',
  standalone: true,
  templateUrl: './administrative-expenses.component.html',
  imports: [
    TranslateModule,
    CollapsableLabelComponent,
    PeriodsComponent
  ]
})
export class AdministrativeExpensesComponent {

}
