import { Component } from "@angular/core";
import { UserCommonComponent } from "../user-common/user-common.component";
import { ActivatedRoute, Router } from "@angular/router";
import { UserResource } from "../../resources/user.resource";
import { UserDto } from "../../models/user.dto";
import { TranslateModule } from "@ngx-translate/core";
import { CommonModule } from "@angular/common";
import { catchError, finalize } from "rxjs";
import { LoadingIndicatorService } from "../../../../infrastructure/components/loading-indicator/services/loading-indicator.service";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: 'user-view',
  standalone: true,
  templateUrl: 'user-view.component.html',
  styleUrls: [],
  imports: [
    UserCommonComponent,
    TranslateModule,
    CommonModule
  ],
  providers: [
    UserResource
  ]
})

export class UserViewComponent {
  model: UserDto = new UserDto();
  private originalModel = new UserDto();

  isEditMode: boolean = false;
  canSubmit: boolean = false;
  isPasswordRequired: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private resource: UserResource,
    private router: Router,
    private loadingIndicator: LoadingIndicatorService,
    private toastrService: ToastrService
  ) { }

  ngOnInit() {
    const userId = this.route.snapshot.params['id'];
    this.getUserById(userId);
  }

  getUserById(id: number) {
    this.resource.getById(id)
      .pipe(
        finalize(() => this.loadingIndicator.hide()),
        catchError(() => this.router.navigate(['/user/search']))
      )
      .subscribe(
        (data: UserDto) => {
          this.model = data;
        }
      );
  }

  enableEditing() {
    this.isEditMode = true;
    this.originalModel = JSON.parse(JSON.stringify(this.model));
  }

  editUser() {
    this.loadingIndicator.show()
    this.resource.update(this.model)
      .pipe(
        finalize(() => { this.loadingIndicator.hide() })
      )
      .subscribe(() => {
        this.isEditMode = false;
        this.toastrService.success("Успешно редактиране на потребител!");
      });
  }

  cancelEdit() {
    this.model = JSON.parse(JSON.stringify(this.originalModel));
    this.finishEdit();
  }

  private finishEdit(): void {
    this.originalModel = null;
    this.isEditMode = false;
  }

  changeFormValidStatus(status: string): void {
    this.canSubmit = status === "VALID";
  }
}