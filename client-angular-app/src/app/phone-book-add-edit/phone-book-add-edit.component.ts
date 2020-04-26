import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { PhoneBookService } from '../phone-book.service';
import { PhoneBook } from '../model/phonebook';

@Component({
  selector: 'app-phone-book-add-edit',
  templateUrl: './phone-book-add-edit.component.html',
  styleUrls: ['./phone-book-add-edit.component.scss']
})
export class PhoneBookAddEditComponent implements OnInit {

  form: FormGroup;
  entries: FormArray;
  actionType: string;

  formName: string;
  id: string;
  errorMessage: any;
  existingPhoneBook: PhoneBook;

  constructor(private phoneBookService: PhoneBookService,
    private formBuilder: FormBuilder,
    private avRoute: ActivatedRoute,
    private router: Router) {
      const idParam = 'id';
      this.actionType = 'Add';
      this.formName = 'name';
      this.id = '';
      if (this.avRoute.snapshot.params[idParam]) {
        this.id = this.avRoute.snapshot.params[idParam];
      }
    }

    ngOnInit() {
      if (this.id.length > 0) {
        this.form = this.formBuilder.group({id: 0, name: ['', [Validators.required]], entries : this.formBuilder.array([])})

        this.actionType = 'Edit';
        this.phoneBookService.getPhoneBook(this.id)
          .subscribe(data => (
            this.existingPhoneBook = data,
            this.form.controls[this.formName].setValue(data.name),
            this.addAllEntries(data.entries)
          ));
      } else {
        this.form = this.formBuilder.group({
            id: 0, name: ['', [Validators.required]], entries : this.formBuilder.array([ this.createEntry() ])
          }
        )
      }
      this.entries = this.form.get('entries') as FormArray;
    }

    save() {
      if (!this.form.valid) {
        return;
      }

      if (this.actionType === 'Add') {

        console.log(this.form.value);
        let phoneBook: PhoneBook = {
          name: this.form.get(this.formName).value,
          entries: this.entries.value
        };

        this.phoneBookService.savePhoneBook(phoneBook)
          .subscribe((data) => {
            this.router.navigate(['/phonebook', data.id]);
          });
      }

      if (this.actionType === 'Edit') {
        let phoneBook: PhoneBook = {
          id: this.existingPhoneBook.id,
          name: this.form.get(this.formName).value,
          entries: this.entries.value
        };

        this.phoneBookService.updatePhoneBook(phoneBook.id, phoneBook)
          .subscribe((data) => {
            this.router.navigate(['/']);
          });
      }
    }

    cancel() {
      this.router.navigate(['/']);
    }

    createEntry(): FormGroup {
      return this.formBuilder.group({
        name: '',
        phoneNumber: ''
      });
    }

    addEntry(): void {
      this.entries.push(this.createEntry());
    }

    removeEntry(currentEntry) : void {
      this.entries.removeAt(currentEntry);
    }

    addAllEntries(entries: any[]): void {
      entries.forEach(entry => {
        this.entries.push(
          this.formBuilder.group({
            name: entry.name,
            phoneNumber: entry.phoneNumber
          })
        );
      });
    }

    removeContact(index) {
      this.entries.removeAt(index);
    }

    getEntriesFormGroup(index): FormGroup {
      this.entries = this.form.get('entries') as FormArray;
      const formGroup = this.entries.controls[index] as FormGroup;
      return formGroup;
    }

    changedFieldType(index) {
      let validators = null;
      validators = Validators.compose([
        Validators.required,
      ]);

      this.getEntriesFormGroup(index).controls['name'].setValidators(
        validators
      );

      this.getEntriesFormGroup(index).controls['name'].updateValueAndValidity();
    }

  get name() { return this.form.get(this.formName); }
  get phoneBookEntries() { return this.form.get('entries') as FormArray; }
}
