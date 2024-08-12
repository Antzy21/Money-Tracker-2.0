import { ref, type Ref } from 'vue'
import { defineStore } from 'pinia'
import type { CategoryGroup } from '@/types/category-group'

export const useCategoryGroupStore = defineStore('categoryGroups', () => {
  const categoryGroups: Ref<CategoryGroup[]> = ref([])

  function setCategoryGroup(data: CategoryGroup) {
    const CategoryGroup = categoryGroups.value.find((cg) => cg.id === data.id)
    if (CategoryGroup) {
      Object.assign(CategoryGroup, data)
    } else {
      categoryGroups.value.push(data)
    }
  }

  function setCategoryGroups(data: CategoryGroup[]) {
    categoryGroups.value = data
  }

  return { categoryGroups, setCategoryGroup, setCategoryGroups }
})
