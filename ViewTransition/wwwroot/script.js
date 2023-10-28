export const beginViewTransition = async () => {
    const PromiseClass = Promise;
    const resolver = {};
    const promise = new PromiseClass((resolve, reject) => {
        resolver.resolve = resolve;
        resolver.reject = reject;
    });

    await new PromiseClass((resolve) => {
        document.startViewTransition(async () => {
            resolve();
            await promise;
        });
    });

    return resolver;
}
