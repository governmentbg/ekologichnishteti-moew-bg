import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { SvgIconComponent } from '../svg-icon/svg-icon.component';

@Component({
  selector: 'app-help-tooltip',
  templateUrl: './help-tooltip.component.html',
  standalone: true,
  imports: [NgbTooltip, SvgIconComponent],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HelpTooltipComponent {
  @Input() icon = 'question-circle';
  @Input() tooltipText: string;
}
