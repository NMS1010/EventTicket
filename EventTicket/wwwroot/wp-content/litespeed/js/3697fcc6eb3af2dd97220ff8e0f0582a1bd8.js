document.addEventListener('wpcf7mailsent',function(event){if(ssk_cf7.contactOrganizerId==event.detail.contactFormId){$("form.wpcf7-form.sent").css('display','none');var hid=document.getElementById("thank-you-contact");hid.classList.remove("d-none")}
if(ssk_cf7.contactUsId==event.detail.contactFormId){$("form.wpcf7-form.sent").css('display','none');var hid=document.getElementById("thank-you");hid.classList.remove("d-none")}
if(ssk_cf7.userPriceId==event.detail.contactFormId){$("form.wpcf7-form.sent").css('display','none');var hid=document.getElementById("thank-you");hid.classList.remove("d-none")}
if(ssk_cf7.visitorRegisterId==event.detail.contactFormId){$("form.wpcf7-form.sent").css('display','none');var hid=document.getElementById("thank-you-visitor");hid.classList.remove("d-none")}
if(ssk_cf7.boothRegisterId==event.detail.contactFormId){$("form.wpcf7-form.sent").css('display','none');var hid=document.getElementById("thank-you-booth");hid.classList.remove("d-none")}},!1)
;