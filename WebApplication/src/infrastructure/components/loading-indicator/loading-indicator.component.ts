import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { LoadingIndicatorService } from './services/loading-indicator.service';
import { Configuration } from '../../configuration/configuration';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-loading-indicator',
  standalone: true,
  templateUrl: './loading-indicator.component.html',
  styleUrls: ['./loading-indicator.component.css'],
  imports: [TranslateModule, CommonModule]
})
export class LoadingIndicatorComponent implements OnDestroy {
  isLoading: boolean;

  private loadingCount = 0;
  private subscription: Subscription;

  constructor(
    private loadingService: LoadingIndicatorService,
    private configuration: Configuration
  ) {
    this.isLoading = false;
    this.loadingCount = 0;

    this.subscription = this.loadingService.subscribe(this.handleLoadingAction.bind(this));
  }

  private handleLoadingAction = (load: boolean) => {
    if (load) {
      this.loadingCount++;
    } else {
      this.loadingCount = this.loadingCount - 1 >= 0 ? this.loadingCount - 1 : 0;
    }

    setTimeout(() => {
      this.isLoading = this.loadingCount !== 0;
    }, this.configuration.loadingTimeoutInSeconds * 1000);
  }

  ngOnDestroy(): void {
    if (this.subscription && !this.subscription.closed) {
      this.subscription.unsubscribe();
    }
  }
}
