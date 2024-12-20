import { Component } from "@angular/core";
import { UserCommonComponent } from "../user-common/user-common.component";
import { TranslateModule } from "@ngx-translate/core";
import { UserResource } from "../../resources/user.resource";
import { Router } from "@angular/router";
import { UserDto } from "../../models/user.dto";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: 'user-new',
  standalone: true,
  templateUrl: 'user-new.component.html',
  styleUrls: [],
  imports: [
    UserCommonComponent,
    TranslateModule
  ],
  providers: [
    UserResource
  ]
})

export class UserNewComponent {
  model: UserDto = new UserDto();

  isEditMode: boolean = true;
  canSubmit: boolean = false;
  isPasswordRequired: boolean = true;

  constructor(
    private resource: UserResource,
    private router: Router,
    private toastrService: ToastrService
  ) { }

  addUser() {
    this.resource.add(this.model)
      .subscribe(() => {
        this.toastrService.success("Успешно добавяне на потребител!");
        this.router.navigate(['/user/search']);
      });
  }

  changeFormValidStatus(status: string): void {
    this.canSubmit = status === "VALID";
  }
}