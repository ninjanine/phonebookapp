import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PhoneBooksComponent } from './phone-book-list/phone-book-list.component';
import { PhoneBookAddEditComponent } from './phone-book-add-edit/phone-book-add-edit.component';
import { PhoneBookService } from "./phone-book.service";

@NgModule({
  declarations: [
    AppComponent,
    PhoneBooksComponent,
    PhoneBookAddEditComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [PhoneBookService],
  bootstrap: [AppComponent]
})
export class AppModule { }
