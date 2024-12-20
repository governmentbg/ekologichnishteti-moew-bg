import { Component } from "@angular/core";
import { UserCommonComponent } from "../user-common/user-common.component";
import { TranslateModule } from "@ngx-translate/core";
import { UserResource } from "../../resources/user.resource";
import { Router } from "@angular/router";
import { UserDto } from "../../models/user.dto";
import { ToastrService } from "ngx-toastr";
import { UserContextService } from "../../services/user-context.service";
import { PasswordChangeModalComponent } from "../../modals/password-change-modal/password-change-modal.component";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { UserPasswordDto } from "../../models/user-password.dto";
import { CommonModule } from "@angular/common";

@Component({
  selector: 'user-profile',
  standalone: true,
  templateUrl: 'user-profile.component.html',
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

export class UserProfileComponent {
  model: UserDto = new UserDto();
  userPasswordDto: UserPasswordDto = new UserPasswordDto();

  isEditMode: boolean = false;
  canSubmit: boolean = false;
  isPasswordRequired: boolean = true;

  constructor(
    private resource: UserResource,
    private router: Router,
    private toastrService: ToastrService,
    private userContextService: UserContextService,
    private modal: NgbModal
  ) { }

  ngOnInit(): void {
    this.resource.getById(this.userContextService.userContext?.userId).subscribe((user) => {
      this.model = user;
      this.userPasswordDto.userId = this.model.id;
    });
  }

  changePassword() {
    const confirmationModal = this.modal.open(PasswordChangeModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = 'Промяна на парола';
    confirmationModal.componentInstance.model = this.userPasswordDto;
    confirmationModal.result
      .then((result: boolean) => {
        this.userPasswordDto = new UserPasswordDto();
      });
  }

  changeFormValidStatus(status: string): void {
    this.canSubmit = status === "VALID";
  }
}