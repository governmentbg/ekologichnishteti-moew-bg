import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-part-panel',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './part-panel.component.html'
})
export class PartPanelComponent {
  @Input() header: string;
  @Input() margin_top: string = "mt-3"
}
