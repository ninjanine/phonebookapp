import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PhoneBookListComponent } from './phone-book-list/phone-book-list.component';
import { PhoneBookAddEditComponent } from './phone-book-add-edit/phone-book-add-edit.component';

@NgModule({
  declarations: [
    AppComponent,
    PhoneBookListComponent,
    PhoneBookAddEditComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
