export interface IInventoryTimeLine {
    timeLine: Date[];
    productInventorySnapshots: IInventorySnapshot[]
}

export interface IInventorySnapshot {
    /*this type of data can tell us  at any given point of time can take a snapshot 
    will know the date and then for each date in the array will have a corresponding snapshot (product inventory snapshot) 
    
    *this is not the only way you can handle it: we can have list of productIDs, qntyOnHand, Date
    *we strutured this way: because the way our apexChart inventory works
    */
    productId: number;
    quantityOnHand: number[];
}