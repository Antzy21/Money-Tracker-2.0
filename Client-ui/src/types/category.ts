export type Category = {
    id: number,
    name: string,
    colour: string,
    regexes: string[]
}

export const Uncategorized: Category = {
    id: 0,
    name: "Uncategorized",
    colour: "#ffffff",
    regexes: [],
}