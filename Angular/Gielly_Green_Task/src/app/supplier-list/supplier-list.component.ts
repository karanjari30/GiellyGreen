import { Component, OnInit } from '@angular/core';
import { faPen, faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { ApiDataService } from '../api-data.service';
@Component({
  selector: 'app-supplier-list',
  templateUrl: './supplier-list.component.html',
  styleUrls: ['./supplier-list.component.css']
})
export class SupplierListComponent implements OnInit {

  editIcon = faPen;
  plusIcon = faPlus;
  deleteIcon = faTrash;
  supplierListData:any;
  isCollapsed = false;
  constructor(private apiService: ApiDataService) { }

  
  ngOnInit(): void {
    this.getDataInTable();
  }

  getDataInTable(){
    this.apiService.getDataSuppliers().subscribe((data:any) => {
      this.supplierListData = data.Result;
      console.log(this.supplierListData);
    })
  }

  deleteSupplier(supplierId:number){
    alert(supplierId);
  }

  editSupplier(supplierId:number){
    alert(supplierId);
  }
 
}
