import { ref, type Ref } from 'vue'
import { defineStore } from 'pinia'
import type { Category } from '@/types/category'

export const useCategoryStore = defineStore('categories', () => {
  const categories: Ref<Category[]> = ref([])

  function setCategory(data: Category) {
    const Category = categories.value.find((c) => c.id === data.id)
    if (Category) {
      Object.assign(Category, data)
    } else {
      categories.value.push(data)
    }
  }

  function setCategories(data: Category[]) {
    categories.value = data
  }

  return { categories, setCategory, setCategories }
})
