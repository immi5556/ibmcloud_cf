document.getElementById("btnLstTable").addEventListener("click", () => {
    dtbl.gettables({
        src: document.getElementById("src-constr").value,
        dest: document.getElementById("dest-constr").value,
        some: ''
    })
});