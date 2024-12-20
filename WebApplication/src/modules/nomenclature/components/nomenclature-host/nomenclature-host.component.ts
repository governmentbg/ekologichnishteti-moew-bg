import { AsyncPipe, CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, RouterLink, RouterOutlet } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'nomenclature-host',
  standalone: true,
  imports: [RouterOutlet, AsyncPipe, CommonModule, NgbModule, RouterLink],
  templateUrl: './nomenclature-host.component.html',
  styleUrl: './nomenclature-host.component.css'
})
export class NomenclatureHostComponent {
  links = [
    { fragment: 1, title: 'Компетентен орган', route: 'authority' },
    { fragment: 2, title: 'Код по КИД на НСИ', route: 'code' },
    { fragment: 3, title: 'Дейност по Приложение № 1', route: 'activity' },
    { fragment: 4, title: 'Поддейност по Приложение № 1', route: 'subActivity' },
    { fragment: 5, title: 'Държава', route: 'country' }
    // { fragment: 6, title: 'Година', route: 'year' }
  ];

  constructor(
    public route: ActivatedRoute
  ) { }

}