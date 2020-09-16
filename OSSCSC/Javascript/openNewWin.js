// JavaScript Document

// JavaScript  -  External Windows Document

function TJKpop(){ // v1.0 | www.TJKDesign.com
  var e = document.getElementById('wrapper');
  if (e){
    var a=e.getElementsByTagName('a');

    for (var i=0;i<a.length;i++){
		
    if (a[i].getAttribute('href') != null && a[i].getAttribute('href').indexOf("http://") >= 0 && a[i].getAttribute('href').toUpperCase().indexOf(document.domain.toUpperCase()) == -1){

        a[i].className+=a[i].className?' outlink':'outlink';
        a[i].title+=' (opens in new window)';
				var rand = Math.round((Math.random() * 10000));
				a[i].onclick=function(){newWin=window.open(this.href,rand);if(window.focus){newWin.focus()} return false;}
        a[i].onkeypress=function(){newWin=window.open(this.href,rand);if(window.focus){newWin.focus()} return false;}
      }
    }
  }
}

function addLoadEvent(func) {
  var oldonload = window.onload;
  if (typeof window.onload != 'function') {
    window.onload = func;
  } else {
    window.onload = function() {
      oldonload();
      func();
    }
  }
}

addLoadEvent(function(){if(document.getElementById) TJKpop();});

// End External Windows Document


// JavaScript  -  PDF Documents Windows


function TJKpopAppPdf(){ 

if(document.getElementById("wrapper"))    {
     var zA=document.getElementById("wrapper").getElementsByTagName("a");
	for (var i=0;i<zA.length;i++){
	// if the type value contains "application/pdf" or if the href value contains "PDF" or if the file is in a "PDF" folder then we have a winner
  if (zA[i].getAttribute("href") != null && zA[i].getAttribute("href").toUpperCase().indexOf(".PDF") >= 0){
		zA[i].title+=" (opens in new window)";
		zA[i].className+=zA[i].className?" pdfFile":"pdfFile";
		// This spawns multiple windows, but works fine with popup blockers
		// zA[i].target="_blank";
		// This opens a unique window and brings it in front of the opener each time the user clicks on the link
		// Note that the new window opens without a toolbar. This is to avoid further conusion for the visitor 
		zA[i].onclick=function() {newWin=window.open(this.href,"TJKWin");if( window.focus){newWin.focus()} return false;}
		zA[i].onkeypress=function() {newWin=window.open(this.href,"TJKWin");if( window.focus){newWin.focus()} return false;}
		}
	}
}
}

function addLoadEvent(func) {
  var oldonload = window.onload;
  if (typeof window.onload != 'function') {
    window.onload = func;
  } else {
    window.onload = function() {
      oldonload();
      func();
    }
  }
}
var isOpera = false;
var agt = navigator.userAgent.toLowerCase();
if(agt.indexOf("opera") > 0) isOpera = true;

if(!isOpera)
	addLoadEvent(function(){if(document.getElementById) TJKpopAppPdf();});

// End PDF Documents Windows