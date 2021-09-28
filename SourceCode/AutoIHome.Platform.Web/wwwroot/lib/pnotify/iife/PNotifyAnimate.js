var _typeof="function"==typeof Symbol&&"symbol"==typeof Symbol.iterator?function(e){return typeof e}:function(e){return e&&"function"==typeof Symbol&&e.constructor===Symbol&&e!==Symbol.prototype?"symbol":typeof e},_extends=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var i in n)Object.prototype.hasOwnProperty.call(n,i)&&(e[i]=n[i])}return e},PNotifyAnimate=function(t){"use strict";t=t&&t.__esModule?t.default:t;var e;function n(e){!function(e,t){e._handlers=a(),e._slots=a(),e._bind=t._bind,e._staged={},e.options=t,e.root=t.root||e,e.store=t.store||e.root.store,t.root||(e._beforecreate=[],e._oncreate=[],e._aftercreate=[])}(this,e),this._state=s(_extends({_notice:null,_options:{}},t.modules.Animate.defaults),e.data),this._intro=!0,this._fragment=(this._state,{c:i,m:i,p:i,d:i}),e.target&&(this._fragment.c(),this._mount(e.target,e.anchor))}function i(){}function s(e,t){for(var n in t)e[n]=t[n];return e}function a(){return Object.create(null)}function o(e){for(;e&&e.length;)e.shift()()}return s(n.prototype,{destroy:function(e){this.destroy=i,this.fire("destroy"),this.set=i,this._fragment.d(!1!==e),this._fragment=null,this._state={}},get:function(){return this._state},fire:function(e,t){var n=e in this._handlers&&this._handlers[e].slice();if(!n)return;for(var i=0;i<n.length;i+=1){var a=n[i];if(!a.__calling)try{a.__calling=!0,a.call(this,t)}finally{a.__calling=!1}}},on:function(e,t){var n=this._handlers[e]||(this._handlers[e]=[]);return n.push(t),{cancel:function(){var e=n.indexOf(t);~e&&n.splice(e,1)}}},set:function(e){if(this._set(s({},e)),this.root._lock)return;!function(e){e._lock=!0,o(e._beforecreate),o(e._oncreate),o(e._aftercreate),e._lock=!1}(this.root)},_set:function(e){var t=this._state,n={},i=!1;for(var a in e=s(this._staged,e),this._staged={},e)this._differs(e[a],t[a])&&(n[a]=i=!0);if(!i)return;this._state=s(s({},t),e),this._recompute(n,this._state),this._bind&&this._bind(n,this._state);this._fragment&&(this.fire("state",{changed:n,current:this._state,previous:t}),this._fragment.p(n,this._state),this.fire("update",{changed:n,current:this._state,previous:t}))},_stage:function(e){s(this._staged,e)},_mount:function(e,t){this._fragment[this._fragment.i?"i":"m"](e,t||null)},_differs:function(e,t){return e!=e?t==t:e!==t||e&&"object"===(void 0===e?"undefined":_typeof(e))||"function"==typeof e}}),s(n.prototype,{initModule:function(e){this.set(e),this.setUpAnimations()},update:function(){this.setUpAnimations()},setUpAnimations:function(){var e=this.get(),t=e._notice,n=e._options;if(e.animate){t.set({animation:"none"}),t._animateIn||(t._animateIn=t.animateIn),t._animateOut||(t._animateOut=t.animateOut),t.animateIn=this.animateIn.bind(this),t.animateOut=this.animateOut.bind(this);var i=250;"slow"===n.animateSpeed?i=400:"fast"===n.animateSpeed?i=100:0<n.animateSpeed&&(i=n.animateSpeed),i/=1e3,t.refs.elem.style.WebkitAnimationDuration=i+"s",t.refs.elem.style.MozAnimationDuration=i+"s",t.refs.elem.style.animationDuration=i+"s"}else t._animateIn&&t._animateOut&&(t.animateIn=t._animateIn,delete t._animateIn,t.animateOut=t._animateOut,delete t._animateOut)},animateIn:function(e){var t=this.get()._notice;function n(){t.refs.elem.removeEventListener("webkitAnimationEnd",n),t.refs.elem.removeEventListener("mozAnimationEnd",n),t.refs.elem.removeEventListener("MSAnimationEnd",n),t.refs.elem.removeEventListener("oanimationend",n),t.refs.elem.removeEventListener("animationend",n),t.set({_animatingClass:"ui-pnotify-in animated"}),e&&e.call(),t.set({_animating:!1})}t.set({_animating:"in"}),t.refs.elem.addEventListener("webkitAnimationEnd",n),t.refs.elem.addEventListener("mozAnimationEnd",n),t.refs.elem.addEventListener("MSAnimationEnd",n),t.refs.elem.addEventListener("oanimationend",n),t.refs.elem.addEventListener("animationend",n),t.set({_animatingClass:"ui-pnotify-in animated "+this.get().inClass})},animateOut:function(e){var t=this.get()._notice;function n(){t.refs.elem.removeEventListener("webkitAnimationEnd",n),t.refs.elem.removeEventListener("mozAnimationEnd",n),t.refs.elem.removeEventListener("MSAnimationEnd",n),t.refs.elem.removeEventListener("oanimationend",n),t.refs.elem.removeEventListener("animationend",n),t.set({_animatingClass:"animated"}),e&&e.call(),t.set({_animating:!1})}t.set({_animating:"out"}),t.refs.elem.addEventListener("webkitAnimationEnd",n),t.refs.elem.addEventListener("mozAnimationEnd",n),t.refs.elem.addEventListener("MSAnimationEnd",n),t.refs.elem.addEventListener("oanimationend",n),t.refs.elem.addEventListener("animationend",n),t.set({_animatingClass:"ui-pnotify-in animated "+this.get().outClass})}}),n.prototype._recompute=i,(e=n).key="Animate",e.defaults={animate:!1,inClass:"",outClass:""},e.init=function(i){return i.attention=function(e,t){function n(){i.refs.container.removeEventListener("webkitAnimationEnd",n),i.refs.container.removeEventListener("mozAnimationEnd",n),i.refs.container.removeEventListener("MSAnimationEnd",n),i.refs.container.removeEventListener("oanimationend",n),i.refs.container.removeEventListener("animationend",n),i.refs.container.classList.remove(e),t&&t.call(i)}i.refs.container.addEventListener("webkitAnimationEnd",n),i.refs.container.addEventListener("mozAnimationEnd",n),i.refs.container.addEventListener("MSAnimationEnd",n),i.refs.container.addEventListener("oanimationend",n),i.refs.container.addEventListener("animationend",n),i.refs.container.classList.add("animated"),i.refs.container.classList.add(e)},new e({target:document.body})},t.modules.Animate=e,n}(PNotify);
//# sourceMappingURL=PNotifyAnimate.js.map