import { Component, OnDestroy, ViewChild, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/main/services/auth.service';
import { APP_CONFIG } from 'src/app/main/configs/environment.config';
import { HeaderComponent } from '../../header/header.component';
import { SidebarComponent } from '../../sidebar/sidebar.component';
import { NgIf } from '@angular/common';
import { Subscription } from 'rxjs';
import { ClientService } from 'src/app/main/services/client.service';
import { Role } from 'src/app/main/enums/roles.enum';

@Component({
  selector: 'app-update-user',
  standalone: true,
  imports: [SidebarComponent, HeaderComponent, ReactiveFormsModule, NgIf],
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.css'],
})
export class UpdateUserComponent implements OnInit, OnDestroy {
  private unsubscribe: Subscription[] = [];

  constructor(
    private _fb: FormBuilder,
    private _service: AuthService,
    private _clientService: ClientService,
    private _toastr: ToastrService,
    private _activateRoute: ActivatedRoute,
    private _router: Router,
    private _toast: NgToastService
  ) {}

  updateForm: FormGroup;
  formValid: boolean = false;
  userId: string;
  updateData: any;
  isupdateProfile: boolean;
  currentLoggedInUser: any;
  headText: string = 'Update User';
  userImage: any = '';
  selectedFile: File;
  previewUrl: string | ArrayBuffer;
  @ViewChild('imageInput') imageInputRef: any;

  ngOnInit(): void {
    // Initialize form with default values
    this.updateForm = this._fb.group({
      id: [''],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNumber: [
        '',
        [Validators.required, Validators.minLength(10), Validators.maxLength(10)],
      ],
      emailAddress: ['', [Validators.required, Validators.email]],
    });

    const url = this._router.url;
    if (url.includes('updateProfile')) {
      this.isupdateProfile = true;
      this.headText = 'Update Profile';
    }

    this.currentLoggedInUser = this._service.getUserDetail();

    // Extract user ID and validate it
    this.userId = this._activateRoute.snapshot.paramMap.get('userId');
    console.log("Extracted userId:", this.userId); // Debugging output

    if (!this.userId) {
      console.error("Error: User ID is missing or invalid in route.");
      this._toast.error({
        detail: 'ERROR',
        summary: 'Invalid User ID',
        duration: APP_CONFIG.toastDuration,
      });
      return; // Stop further execution
    }

    if (this.currentLoggedInUser) {
      const currentRole = this.currentLoggedInUser.userType;
      if (currentRole !== Role.Admin && this.userId !== this.currentLoggedInUser.userId) {
        this._toast.error({
          detail: 'ERROR',
          summary: 'You are not authorized to access this page',
          duration: APP_CONFIG.toastDuration,
        });
        history.back();
      }
    }

    // Fetch user details
    this.fetchDetail(this.userId);
}


fetchDetail(id: any) {
  const getUserSubscribe = this._clientService.loginUserDetailById(id).subscribe(
    (data: any) => {
      console.log("Received API response:", data);

      if (data && Object.keys(data).length > 0) {
        this.updateData = data; // Assign response directly

        this.updateForm.patchValue({
          id: this.updateData.id || '',
          firstName: this.updateData.firstName || '',
          lastName: this.updateData.lastName || '',
          phoneNumber: this.updateData.phoneNumber || '',
          emailAddress: this.updateData.emailAddress || '',
        });
      } else {
        console.warn("Unexpected empty user data:", data);
      }
    },
    (err) => console.error("API error:", err)
  );

  this.unsubscribe.push(getUserSubscribe);
}


  onSubmit() {
    console.log("Submitting Form....");
    this.formValid = true;
    if (this.updateForm.valid) {
      console.log("Form is valid, submitting...");
      const formData = new FormData();
      const updatedUserData = this.updateForm.getRawValue();
      // Object.keys(updatedUserData).forEach((key) => {
      //   formData.append(key, updatedUserData[key]);
      // });
      formData.append('Id', updatedUserData.id);
        formData.append('FirstName', updatedUserData.firstName);
        formData.append('LastName', updatedUserData.lastName);
        formData.append('PhoneNumber', updatedUserData.phoneNumber);
        formData.append('EmailAddress', updatedUserData.emailAddress);

        if (this.selectedFile) {
    formData.append('UserImage', this.selectedFile.name); // Send file name as a string
  } else if (this.updateData?.userImage) {
    formData.append('UserImage', this.updateData.userImage);
  }
          console.log("FormData contents:");
        formData.forEach((value, key) => {
  console.log(`${key}:`, value);
});


      const updateUserSubscribe = this._service.updateUser(formData).subscribe(
        (data: any) => {
          if (data.result === 1) {
            this._toast.success({
              detail: 'SUCCESS',
              summary: this.isupdateProfile ? 'Profile Updated Successfully' : data.data,
              duration: APP_CONFIG.toastDuration,
            });
            setTimeout(() => {
              this._router.navigate([this.isupdateProfile ? 'admin/profile' : 'admin/user']);
            }, 1000);
          } else {
            this._toastr.error(data.message);
          }
        },
        (err) =>
          this._toast.error({
            detail: 'ERROR',
            summary: err.message,
            duration: APP_CONFIG.toastDuration,
          })
      );

      this.formValid = false;
      this.unsubscribe.push(updateUserSubscribe);
    }else{
      console.error("Form validation failed.",this.updateForm.errors);
      Object.keys(this.updateForm.controls).forEach(field => {
      const control = this.updateForm.get(field);
      if (control?.invalid) {
        console.error(`Field '${field}' has error:`, control.errors);
      }
    });
    }
  }

  onCancel() {
    this._router.navigate([this.isupdateProfile ? 'admin/profile' : 'admin/user']);
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      const reader = new FileReader();
      reader.onload = () => (this.previewUrl = reader.result);
      reader.readAsDataURL(file);
    }
  }

  getFullImageUrl(imagePath: string): string {
    return imagePath ? `${APP_CONFIG.imageBaseUrl}/${imagePath}` : '';
  }

  triggerImageInput(): void {
    this.imageInputRef.nativeElement.click();
  }

  cancelImageChange(): void {
    this.selectedFile = null;
    this.previewUrl = null;
    this.updateData.profileImage = null;
  }

  onImageError(event: any): void {
    event.target.src = 'assets/Images/default-user.png';
  }

  ngOnDestroy() {
    this.unsubscribe.forEach((sb) => sb.unsubscribe());
  }

  /** Getter functions to access form controls **/
  get firstName(): FormControl {
    return this.updateForm.get('firstName') as FormControl;
  }

  get lastName(): FormControl {
    return this.updateForm.get('lastName') as FormControl;
  }

  get phoneNumber(): FormControl {
    return this.updateForm.get('phoneNumber') as FormControl;
  }

  get emailAddress(): FormControl {
    return this.updateForm.get('emailAddress') as FormControl;
  }
}
