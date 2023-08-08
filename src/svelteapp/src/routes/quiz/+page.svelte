<script lang="ts">
  import { goto } from "$app/navigation";
  import { onMount } from "svelte";
  import { testStore } from "$lib/stores";
  import AppHeading from "$lib/components/AppHeading.svelte";
  import AppVariant from "$lib/components/AppVariant.svelte";

  let test = $testStore;

  onMount(async () => {
    if (test) {
      await goto("/quiz/settings");
    }
  });

</script>

<AppHeading>{test?.string}</AppHeading>

<div class="flex flex-col">
  <AppVariant>
  </AppVariant>
</div>
