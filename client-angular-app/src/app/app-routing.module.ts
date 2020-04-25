import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PhoneBooksComponent } from "./phone-book-list/phone-book-list.component";
import { PhoneBookAddEditComponent } from "./phone-book-add-edit/phone-book-add-edit.component";


const routes: Routes = [
  { path: '', component: PhoneBooksComponent, pathMatch: 'full' },
  { path: 'add', component: PhoneBookAddEditComponent },
  { path: 'edit/:id', component: PhoneBookAddEditComponent },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
