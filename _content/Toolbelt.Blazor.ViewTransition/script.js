export const beginViewTransition = async () => {
    const doc = document;
    const PromiseClass = Promise;
    const resolver = {};
    const startViewTransition = doc.startViewTransition?.bind(doc);
    const promise = new PromiseClass((resolve, reject) => {
        resolver.resolve = resolve;
        resolver.reject = reject;
    });

    if (startViewTransition) {
        await new PromiseClass((resolve) => {
            startViewTransition(async () => {
                resolve();
                await promise;
            });
        });
    }

    return resolver;
}
