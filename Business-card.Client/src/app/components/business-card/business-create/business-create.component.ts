// business-create.component.ts
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SharedapiService } from '../../../sharedapi.service';
import Swal from 'sweetalert2'; // If you're using SweetAlert for notifications

@Component({
  selector: 'app-business-create',
  templateUrl: './business-create.component.html',
  styleUrls: ['./business-create.component.css']
})
export class BusinessCreateComponent {
  form: FormGroup;
  imgURL: any;
  base64String: string = '';
  errorMessage: string = '';
  
 

  constructor(private fb: FormBuilder, private sharedapiService: SharedapiService) {
    this.form = this.fb.group({
      businessCard_Name: ['', Validators.required],
      businessCard_PhoneNumber: ['',Validators.required],
      businessCard_Email: ['',Validators.required],
      businessCard_Date_Of_Birth: ['', Validators.required],
      businessCard_Gender: ['', Validators.required], 
      businessCard_Address: ['',Validators.required],
      photo: ['',Validators.required]
    });
  }

  // Handle file input change event
  onFileChange(event: any) {
    const file = event.target.files[0];

    if (file) {
      const reader = new FileReader();

      reader.onload = (e: any) => {
        this.imgURL = e.target.result;  // Preview image in the frontend
        this.base64String = e.target.result.split(',')[1];  // Extract Base64 string
        this.form.patchValue({
          photo: this.base64String  // Update form control
        });
      };

      reader.readAsDataURL(file);
    }
  }

  // Submit the form
  submitForm() {
    if (this.form.valid) {
        const formData = this.form.value;
        console.log('Form Data:', formData); // Log the data being sent

        // Show confirmation SweetAlert before submission
        Swal.fire({
            title: 'Are you sure?',
            html: `<strong>Business Card Details:</strong><br/>
                   Name: ${formData.businessCard_Name}<br/>
                   Email: ${formData.businessCard_Email}<br/>
                   Phone: ${formData.businessCard_PhoneNumber}<br/>
                   Date of Birth: ${formData.businessCard_Date_Of_Birth}<br/>
                   Gender: ${formData.businessCard_gender}<br/> 
                    Address: ${formData.businessCard_Address}<br/>
                   `,


                   
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, submit',
            cancelButtonText: 'No, cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                // Proceed with form submission if confirmed
                this.sharedapiService.AddBusinessCard(formData).subscribe({
                    next: (response: any) => {
                        console.log('Business card saved successfully:', response);

                        // Show success message
                        Swal.fire({
                            title: 'Business Card Saved!',
                            html: `<p><strong>Business Card Details:</strong><br/>
                            Name: ${formData.businessCard_Name}<br/>
                            Email: ${formData.businessCard_Email}<br/>
                            Phone: ${formData.businessCard_PhoneNumber}<br/>
                            Date of Birth: ${formData.businessCard_Date_Of_Birth}<br/>
                            Gender: ${formData.businessCard_gender}<br/> 
                            Address: ${formData.businessCard_Address}<br/>`,
                            icon: 'success',
                            confirmButtonText: 'OK'
                        });
                    },
                    error: (error: any) => {
                        console.error('Error saving business card:', error);

                        // Show error message
                        Swal.fire({
                            title: 'Error!',
                            text: 'There was an issue saving the business card. Please try again.',
                            icon: 'error',
                            confirmButtonText: 'OK'
                        });
                    }
                });
            }
        });
    } else {
        console.error('Form is invalid:', this.form.errors);

        // Show error message for invalid form
        Swal.fire({
            title: 'Form Error!',
            text: 'Please fill out all required fields correctly.',
            icon: 'error',
            confirmButtonText: 'OK'
        });
    }
}

  

}
