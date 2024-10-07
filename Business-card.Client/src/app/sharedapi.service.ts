
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class SharedapiService {
    constructor(private http: HttpClient) {}

    GetBusinessAll() {
        return this.http.get('https://localhost:7109/api/BusinessCard/GetAllBusinessCard');
    }

    // Method to save the business card
    AddBusinessCard(businessCard: any) {
        return this.http.post('https://localhost:7109/api/BusinessCard/AddBusinessCard', businessCard);
    }

    ImportBusinessCardFromFile(formData: FormData) {
      return this.http.post('https://localhost:7109/api/BusinessCard/ImportBusinessCardFromFile', formData);
    }

    // Method to delete a business card
    DeleteBusinessCardByID(id: number): Observable<any> {
      return this.http.delete(`https://localhost:7109/api/BusinessCard/DeleteBusinessCardByID?id=${id}`);
    }

    

    
}





