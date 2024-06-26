﻿const toggleMenu = () => {
    console.log('Hamburger button clicked');
    if (window.innerWidth >= 1200) {
        document.getElementById('mobile-menu').classList.toggle('hide');
        document.getElementById('mobile-account-buttons').classList.toggle('hide');
    } else {
        document.getElementById('mobile-menu').classList.toggle('hide');
        document.getElementById('mobile-account-buttons').classList.toggle('hide');
    }
}



const checkScreenSize = () => {
    if (window.innerWidth >= 1200) {
        document.getElementById('menu').classList.remove('hide');
        document.getElementById('account-buttons').classList.remove('hide');
    } else {
        if (!document.getElementById('menu').classList.contains('hide')) {
            document.getElementById('menu').classList.add('hide');
        }
        if (!document.getElementById('account-buttons').classList.contains('hide')) {
            document.getElementById('account-buttons').classList.add('hide');
        }
    }
};




