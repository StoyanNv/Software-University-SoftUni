function printPlayingTrack(params) {
    let trackName = params[0];
    let artistName = params[1];
    let duration = params[2];

    return `Now Playing: ${artistName} - ${trackName} [${duration}]`;
}