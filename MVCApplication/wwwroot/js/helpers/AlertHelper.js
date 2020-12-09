/**
 *  Swal.fire Alert Dialog -
 * @param {*} data -
 * 
 *  ```
 *  {
 *       title: 'Are you sure?',
 *       text: 'You will not be able to recover this imaginary file!',
 *       icon: 'warning',
 *       showCancelButton: true,
 *       confirmButtonText: 'Yes, delete it!',
 *       cancelButtonText: 'No, keep it'
 *   }
 * ```
 */
export const customAlert = async  (data) => {
    return Swal.fire(data)
}