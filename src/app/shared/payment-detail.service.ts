
import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { environment } from 'src/environments/environment';

import { PaymentDetail } from './payment-detail.model';

import { NgForm } from '@angular/forms';


@Injectable({
  providedIn: 'root'
})


export class PaymentDetailService {

  getAllUrl : string = environment.getAllPaymentDetail_Url;

  postUrl : string = environment.postPaymentDetail_Url;

  putUrl : string = environment.putPaymentDetail_Url;

  deleteUrl : string = environment.deletePaymentDetail_Url;

  list : PaymentDetail[] = [];

  formData : PaymentDetail = new PaymentDetail();

  formSubmitted : boolean = false;


  constructor(private httpClient: HttpClient) {}


  getAllPaymentDetail()
  {
    this.httpClient.get(this.getAllUrl)
    .subscribe({
      next: serverResponse => {
        this.list = serverResponse as PaymentDetail[]
      },
      error: errorMessage => { console.log(errorMessage) }
    })
  }


  postPaymentDetail()
  {
    return this.httpClient.post(this.postUrl,this.formData);
  }

  putPaymentDetail()
  {
    return this.httpClient.put(`${this.putUrl} ${this.formData.paymentDetailId}`, this.formData);
  }


  deletePaymentDetail(id : number)
  {
    return this.httpClient.delete(`${this.deleteUrl} ${id}`);
  }

  
  resetForm(form: NgForm)
  {
    form.form.reset();
    this.formData = new PaymentDetail();
    this.formSubmitted = false;
  }
}
