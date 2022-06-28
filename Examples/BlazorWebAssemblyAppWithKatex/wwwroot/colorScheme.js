export function prefersLightMode() {
    return window.matchMedia && window.matchMedia('(prefers-color-scheme: light)').matches;
}
