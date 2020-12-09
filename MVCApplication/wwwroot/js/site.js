// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

import { customAlert } from './helpers/AlertHelper.js'
// Write your JavaScript code.
(async ($, document, window) => {

    const alertResult = await customAlert({
        title: 'Are you sure?',
        text: 'You will not be able to recover this imaginary file!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, keep it'
    })

    console.log(alertResult)
    
    $('#fuckBtn').on('click', async function () {
        try {
            const req = await fetch('http://localhost:59487/api/cardreader')
            const res = await req.json()
            alert(JSON.stringify(res))
        } catch (e) {
            alert(e)
        }
    })
})($, document, window)