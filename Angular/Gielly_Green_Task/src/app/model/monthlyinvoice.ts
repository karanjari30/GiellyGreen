export interface Monthlyinvoice {
    InvoiceId: number;
    Custom1: string;
    Custom2: string;
    Custom3: string;
    Custom4: string;
    Custom5: string;
    InvoiceReferenceId: string;
    InvoiceYear: number;
    InvoiceMonth: number;
    InvoiceDate: string;
    InvoiceViewList: 
    [
        {
            MontlyInvoiceId: number;
            HairService: number;
            BeautyService: number;
            CustomHeader1: number;
            CustomHeader2: number;
            CustomHeader3: number;
            CustomHeader4: number;
            CustomHeader5: number;
            NetAmount: number;
            VATAmount: number;
            GrossAmount: number;
            AdvancePay: number;
            BalanceDue: number;
            IsApprove: boolean;
            SupplierId: number;
            InvoiceId: number;
        },
    ]
}