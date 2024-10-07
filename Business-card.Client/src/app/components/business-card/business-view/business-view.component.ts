import { Component } from '@angular/core';
import { SharedapiService } from '../../../sharedapi.service';
import Swal from 'sweetalert2'; // If you're using SweetAlert for notifications

@Component({
  selector: 'app-business-view',
  templateUrl: './business-view.component.html',
  styleUrls: ['./business-view.component.css'] // Fix typo from styleUrl to styleUrls
})
export class BusinessViewComponent {
  selectedFile: File | null = null; // Variable to hold the selected file
  cards: any[] | undefined;
  card: any;

  constructor(private apiService: SharedapiService) {}

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.apiService.GetBusinessAll().subscribe(
      (response: any) => {
        this.cards = response; 
        console.log(this.cards);
      }
    );
  }
  

  // Method to handle file selection
  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    }
  }

  // Method to handle file upload
uploadFiles() {
    if (this.selectedFile) {
        const fileName = this.selectedFile.name; // Get the selected file's name

        // Show confirmation SweetAlert before file upload
        Swal.fire({
            title: 'Are you sure?',
            html: `<p>You are about to upload the following file: <strong>${fileName}</strong></p>`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, upload it',
            cancelButtonText: 'No, cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                const formData: FormData = new FormData();
                if (this.selectedFile) {
                  formData.append('file', this.selectedFile);
                }

                // Proceed with file upload after confirmation
                this.apiService.ImportBusinessCardFromFile(formData).subscribe(
                    
                  (response: any) => {
                        console.log('Business card saved successfully:', response);

                        // Show success message
                        Swal.fire({
                            title: 'Success!',
                            text: 'The file has been uploaded and processed successfully.',
                            icon: 'success',
                            confirmButtonText: 'OK'
                        });

                        // Optionally, refresh data after successful upload
                        this.getData();
                    },
                    (error: any) => {
                        console.error('Error saving business card:', error);

                        // Show error message
                        Swal.fire({
                            title: 'Error!',
                            text: 'There was an issue uploading the file. Please try again.',
                            icon: 'error',
                            confirmButtonText: 'OK'
                        });
                    }
                );
            }
        });
    } else {
        // Show error if no file is selected
        Swal.fire('Error', 'Please select a file to upload', 'error');
    }
}

  ConvertFromBase64(imgUrl: string): string {
    if (imgUrl) {
      return `data:image/jpeg;base64,${imgUrl}`;
    }
    return '';


  }

   //Method to delete a business card
   deleteBusinessCard(id: number) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this business card!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it'
    }).then((result) => {
      if (result.isConfirmed) {
        this.apiService.DeleteBusinessCardByID(id).subscribe(
          (response) => {
            console.log('Business card deleted successfully:', response);
            Swal.fire('Deleted!', 'Your business card has been deleted.', 'success');
            this.getData();
          },
          (error) => {
            console.error('Error deleting business card:', error);
            Swal.fire('Error', `There was a problem deleting the business card: ${error.message}`, 'error');
          }
        );
      }
    });
  }
  

  
  

}
