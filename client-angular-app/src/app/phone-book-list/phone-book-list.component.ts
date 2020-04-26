
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { PhoneBook } from '../model/phonebook';
import { PhoneBookService } from '../phone-book.service';

@Component({
  selector: 'app-phone-books',
  templateUrl: './phone-book-list.component.html',
  styleUrls: ['./phone-book-list.component.scss']
})
export class PhoneBooksComponent implements OnInit {
  phoneBooks$: Observable<PhoneBook[]>;
  hasSearched : boolean = false;
  constructor(private phoneBookService: PhoneBookService) { }

  ngOnInit(): void {
    this.loadAllPhoneBooks();
  }

  loadAllPhoneBooks() {
    this.phoneBooks$ = this.phoneBookService.getPhoneBooks();
  }

  search(searchInput :string) {
    if (searchInput){
      this.phoneBooks$ = this.phoneBookService.getSearchResult(searchInput);
      this.hasSearched = true;
    }
  }

  delete(phoneBook) {
    const ans = confirm('Do you want to delete phone book contact: ' + phoneBook.name);
    if (ans) {
      this.phoneBookService.deletePhoneBook(phoneBook.id).subscribe((data) => {
        this.loadAllPhoneBooks();
      });
    }
  }

}
