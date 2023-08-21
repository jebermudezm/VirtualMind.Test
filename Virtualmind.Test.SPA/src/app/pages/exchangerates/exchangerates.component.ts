import { Component } from '@angular/core';
import { ApiService } from 'src/app/services/apiservice.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-exchangerates',
  templateUrl: './exchangerates.component.html',
  styleUrls: ['./exchangerates.component.css']
})
export class ExchangeratesComponent {
  exchangeRates: any = {};

  constructor(private apiService: ApiService, private spinner: NgxSpinnerService, private toastr: ToastrService,) {
  }

  ngOnInit(): void {
    this.  refreshRates();
  }

  refreshRates(): void {
    this.getExchangeRate("USD");
  }

   getExchangeRate(currencyCode: string) {
    this.spinner.show();
    this.apiService.getExchangeRate(currencyCode).subscribe(
      {
        next: (data) => {
          if (currencyCode == "USD") {
            this.exchangeRates.USD = data.rate;
            console.log("USD",data);
            this.getExchangeRate("BRL");
          }
          else if (currencyCode == "BRL") {
            this.exchangeRates.BRL = data.rate;
            console.log("BRL",data);
            this.spinner.hide();
          }
        },
        error: (err: any) => { 
          this.spinner.hide();
          console.error(err);
        },
        complete: () => { 
          if (currencyCode == "BRL")
            this.spinner.hide();
        }
      }
    );
  }

}
