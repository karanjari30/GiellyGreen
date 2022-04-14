import { Component, OnInit } from '@angular/core';
import { faPen, faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-supplier-list',
  templateUrl: './supplier-list.component.html',
  styleUrls: ['./supplier-list.component.css']
})
export class SupplierListComponent implements OnInit {

  editIcon = faPen;
  plusIcon = faPlus;
  deleteIcon = faTrash;
  isCollapsed = false;
  constructor() { }

  
  ngOnInit(): void {
  }
  
  listOfData: any[] = [
    {
      name: 'John Brown',
      chinese: 98,
      math: 60,
      english: 70
    },
    {
      name: 'Jim Green',
      chinese: 98,
      math: 66,
      english: 89
    },
    {
      name: 'Joe Black',
      chinese: 98,
      math: 90,
      english: 70
    },
    {
      name: 'Jim Red',
      chinese: 88,
      math: 99,
      english: 89
    },
    {
      name: 'Jim Red',
      chinese: 88,
      math: 99,
      english: 89
    },
    {
      name: 'Jim Red',
      chinese: 88,
      math: 99,
      english: 89
    },
    {
      name: 'Jim Red',
      chinese: 88,
      math: 99,
      english: 89
    },
    {
      name: 'Jim Red',
      chinese: 88,
      math: 99,
      english: 89
    },
    {
      name: 'Jim Red',
      chinese: 88,
      math: 99,
      english: 89
    },
    {
      name: 'Jim Red',
      chinese: 88,
      math: 99,
      english: 89
    },
    {
      name: 'Jim Red',
      chinese: 88,
      math: 99,
      english: 89
    },
    {
      name: 'Jim Red',
      chinese: 88,
      math: 99,
      english: 89
    },

  ];

}
