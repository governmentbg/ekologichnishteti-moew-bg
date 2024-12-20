import { CommonModule } from '@angular/common';
import { Component, Input, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbCollapse, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SvgIconComponent } from '../svg-icon/svg-icon.component';

@Component({
  selector: 'collapsable-label',
  standalone: true,
  imports: [FormsModule, CommonModule, NgbModule, TranslateModule, SvgIconComponent],
  templateUrl: './collapsable-label.component.html',
  styleUrl: './collapsable-label.component.css'
})
export class CollapsableLabelComponent {
  @ViewChild('collapse') collapseElement: NgbCollapse;
  @Input() heading: string;
  @Input() isCollapsed = true;
  @Input() disabled = false;
  @Input() icon: string;
  @Input() fontSize = 'fs-15'
  @Input() color = 'black';
  @Input() collapsableLabelColor = 'collapsable-label'

  onToggle() {
    if (!this.disabled) {
      this.collapseElement.toggle();
    }
  }
}
