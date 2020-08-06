var dtbl = (function () {

    var comparetable = (data) => {

    }
    var srctblst = document.getElementById("src-tbl");
    var desttblst = document.getElementById("dest-tbl");
    var srctblcol = document.getElementById("src-tbl-col");
    var desttblcol = document.getElementById("dest-tbl-col");
    srctblst.addEventListener("change", (evt) => {
        srctblcol.innerHTML = "";
        var rdat = JSON.parse(evt.target.selectedOptions[0].dataset.cl_clms);
        rdat.forEach(t => {
            const option = new Option(t, t);
            srctblcol.add(option, undefined);
        });
    });
    desttblst.addEventListener("change", (evt) => {
        desttblcol.innerHTML = "";
        var rdat = JSON.parse(evt.target.selectedOptions[0].dataset.cl_clms);
        rdat.forEach(t => {
            const option = new Option(t, t);
            desttblcol.add(option, undefined);
        });
    });
    var gettables = (data) => {
        api.postJson('api/compare/tables', data).then((resp) => {
            console.log(resp);
            let intersection = (resp.srctbls || []).filter(x => (resp.desttbls || []).includes(x));
            let srcdiff = (resp.srctbls || []).filter(x => !intersection.includes(x));
            let destdiff = (resp.desttbls || []).filter(x => !intersection.includes(x));
            //var arr = [];
            //if ((resp.srctbls || []).length > (resp.desttbls || []).length) {
            //    arr = (resp.srctbls || []).map((itm, idx) => {
            //        return {
            //            srcname: itm,
            //            destname: resp.desttbls[idx] || ''
            //        }
            //    });
            //} else {
            //    arr = (resp.desttbls || []).map((itm, idx) => {
            //        return {
            //            destname: itm,
            //            srcname: resp.srctbls[idx] || ''
            //        }
            //    });
            //}
            //var carr = (intersection || []).map(itm => {
            //    return { name: itm };
            //});
            //var tlc = document.getElementById("tbllst");
            //var tb = new TableBuilder({
            //    data: carr,
            //    title: "Table Lists (Common Name)",
            //    config: {
            //        roweditable: false,
            //    },
            //    onrowclick: (rdata) => {
            //    },
            //    columns: [
            //        {
            //            Property: "name",
            //            Display: "Table Name"
            //        },
            //        {
            //            Property: "CustomProperty",
            //            Display: "Show DIff",
            //            type: 'Button',
            //            onevent: (col, rdata) => {
                            
            //            }
            //        }
            //    ]
            //});
            srctblst.innerHTML = "";
            (resp.srctbls || []).forEach(t => {
                const option = new Option(t, t);
                option.dataset.cl_clms = JSON.stringify(resp.srctblclmns[t] || []);
                srctblst.add(option, undefined);
            });
            desttblst.innerHTML = "";
            (resp.desttbls || []).forEach(t => {
                const option = new Option(t, t);
                option.dataset.cl_clms = JSON.stringify(resp.desttblclmns[t] || []);
                desttblst.add(option, undefined);
            });

            //var tbdiff = new TableBuilder({
            //    data: arr,
            //    title: "Table Lists (Diff Name)",
            //    config: {
            //        roweditable: false,
            //    },
            //    onrowclick: (rdata) => {
            //    },
            //    columns: [
            //        {
            //            Property: "srcname",
            //            Display: "Src Table"
            //        },
            //        {
            //            Property: "destname",
            //            Display: "Dest Table"
            //        },
            //        {
            //            Property: "CustomProperty",
            //            Display: "Show DIff",
            //            type: 'Button',
            //            onevent: (col, rdata) => {
            //                data.comparetbl = rdata;
            //                comparetable(data);
            //            }
            //        }
            //    ]
            //});
        });
    }
    return {
        gettables: gettables
    }
})();