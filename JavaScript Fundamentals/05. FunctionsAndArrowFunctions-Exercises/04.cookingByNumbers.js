function cookNumbers(params) {
    let chop = x => x / 2;
    let dice = x => Math.sqrt(x);
    let spice = x => x + 1;
    let bake = x => x * 3;
    let fillet = x => x * 0.8;

    let n = params[0];
    for (let i = 1; i < params.length; i++) {
        if(params[i] ==='chop' ){
            console.log(chop(n));
            n = chop(n)
        }if(params[i] ==='dice' ){
            console.log(dice(n));
            n = dice(n)
        }if(params[i] ==='spice' ){
            console.log(spice(n));
            n = spice(n)
        }if(params[i] ==='bake' ){
            console.log(bake(n));
            n = bake(n)
        }if(params[i] ==='fillet' ){
            console.log(fillet(n));
            n = fillet(n)
        }
    }
}