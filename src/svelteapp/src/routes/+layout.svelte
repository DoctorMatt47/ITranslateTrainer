<script>
  import "../app.css";
  import "../app.postcss";
  import { AppBar, AppShell } from "@skeletonlabs/skeleton";
  import AppCard from "$lib/common/components/AppCard.svelte";

  const { children } = $props();
</script>

<AppShell>
  <AppBar slot="header">
    <svelte:fragment slot="lead">
      <i class="fa-solid fa-language"></i>
    </svelte:fragment>
    ITranslateTrainer
    <svelte:fragment slot="trail">
      <a href="/menu"><i class="fa-solid fa-home"></i></a>
    </svelte:fragment>
  </AppBar>
  <div class="container mx-auto mt-8">
    <AppCard>
      <div class="flex flex-col gap-y-6">
        {@render children()}
      </div>
    </AppCard>
  </div>
</AppShell>

<style></style>
