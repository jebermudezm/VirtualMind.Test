import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ApiService } from 'src/app/services/apiservice.service';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.css']
})
export class PurchaseComponent {

  userId: string = '';
  currency: string = '';
  amount: number = 0;
  result: any;
  
  constructor(private apiService: ApiService,public toastr: ToastrService,private spinner: NgxSpinnerService) {}
  
  makePurchase(): void {
    const parameters = {
      userId: this.userId,
      currencyCode: this.currency,
      amountInPeso: this.amount
    };
    this.spinner.show();
    this.apiService.purchaseCurrency(parameters).subscribe(
      {
        next: (data) => {
          this.result = data;
          this.toastr.success('The transaction was created successfully!', 'Success', {
            timeOut: 3000,
          });
        },
        error: (err: any) => { 
          this.spinner.hide();
          console.error(err);
          this.toastr.error(`Error: ' ${err}`, 'Error', {
            timeOut: 3000,
          });
        },
        complete: () => { 
            this.spinner.hide();
        }
      }
    );
  }

}
