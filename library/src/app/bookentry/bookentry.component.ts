import { NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-bookentry',
  standalone: true,
  imports: [NgFor, NgIf, FormsModule],
  templateUrl: './bookentry.component.html',
  styleUrls: ['./bookentry.component.css']
})
export class BookentryComponent {
  title = 'library';
  name = 'Neel';
  bookname = '';
  books: { name: string, isrented: boolean }[] = [];

  addItem() {
    if (this.bookname.trim() !== '') {
      this.books.push({ name: this.bookname, isrented: false });
      this.bookname = '';
    }
  }

  deleteItem(book: any) {
    this.books = this.books.filter(i => i.name !== book.name);
  }

  updateItem(book: any) {
    this.deleteItem(book);
    this.bookname = book.name;
  }
}
